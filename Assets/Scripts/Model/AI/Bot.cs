using System;
using UnityEngine;
using UnityEngine.AI;


namespace GeekBrainsFPS
{
    public sealed class Bot : BaseObjectScene, IExecute, IPointsGiver
    {
        #region PrivateData

        public event Action<Bot> OnDieChange;
        public event Action<IPointsGiver> OnPointChange = delegate { };

        #endregion


        #region Fields

        public Vision Vision;
        public Weapon Weapon; //todo с разным оружием
        public Transform Target { get; set; }
        public NavMeshAgent Agent { get; private set; }

        [SerializeField] private int _points = 3;

        public float Hp = 100;

        private StateBot _stateBot;
        private Vector3 _point;
        private ITimeRemaining _timeRemaining;

        private float _waitTime = 3;
        private float _stoppingDistance = 2.0f;

        #endregion


        #region Properties

        private StateBot StateBot
        {
            get => _stateBot;
            set
            {
                _stateBot = value;
                switch (value)
                {
                    case StateBot.None:
                        Color = Color.white;
                        break;
                    case StateBot.Patrol:
                        Color = Color.green;
                        break;
                    case StateBot.Inspection:
                        Color = Color.yellow;
                        break;
                    case StateBot.Detected:
                        Color = Color.red;
                        break;
                    case StateBot.Died:
                        Color = Color.gray;
                        break;
                    default:
                        Color = Color.white;
                        break;
                }
            }
        }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            Agent = GetComponent<NavMeshAgent>();
            _timeRemaining = new TimeRemaining(ResetStateBot, _waitTime);
        }

        private void OnEnable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange += SetDamage;

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null) headBot.OnApplyDamageChange += SetDamage;
        }

        private void OnDisable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange -= SetDamage;

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null) headBot.OnApplyDamageChange -= SetDamage;
        }

        #endregion


        #region Methods

        public int GivePoints()
        {
            return _points;
        }

        private void ResetStateBot()
        {
            StateBot = StateBot.None;
        }

        private void SetDamage(InfoCollision info)
        {
            if (Hp > 0)
            {
                GetAngry();
                Hp -= info.Damage;
            }

            if (Hp <= 0)
            {
                StateBot = StateBot.Died;
                Agent.enabled = false;
                foreach (var child in GetComponentsInChildren<Transform>())
                {
                    child.parent = null;

                    var tempRbChild = child.GetComponent<Rigidbody>();
                    if (!tempRbChild)
                    {
                        tempRbChild = child.gameObject.AddComponent<Rigidbody>();
                    }
                    tempRbChild.AddForce(info.Dir * UnityEngine.Random.Range(10, 20));

                    Destroy(child.gameObject, 5);
                }

                OnDieChange?.Invoke(this);
                OnPointChange?.Invoke(this);
            }
        }

        public void MovePoint(Vector3 point)
        {
            Agent.SetDestination(point);
        }

        public void Inspect()
        {
            Agent.ResetPath();
            StateBot = StateBot.Inspection;
            _timeRemaining.AddTimeRemaining();
        }

        public void GetAngry()
        {
            StateBot = StateBot.Detected;
            if (Math.Abs(Agent.stoppingDistance - _stoppingDistance) > Mathf.Epsilon)
            {
                Agent.stoppingDistance = _stoppingDistance;
            }
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (StateBot == StateBot.Died) return;

            if (StateBot != StateBot.Detected)
            {
                if (!Agent.hasPath)
                {
                    if (StateBot != StateBot.Inspection)
                    {
                        if (StateBot != StateBot.Patrol)
                        {
                            StateBot = StateBot.Patrol;
                            _point = Patrol.GenericPoint(transform);
                            MovePoint(_point);
                            Agent.stoppingDistance = 0.0f;
                        }
                        else
                        {
                            if ((_point - transform.position).sqrMagnitude <= 1)
                            {
                                Inspect();
                            }
                        }
                    }
                }

                if (Vision.VisionM(transform, Target))
                {
                    GetAngry();
                }
            }
            else
            {
                if ((transform.position - Target.position).sqrMagnitude <= Vision.ActiveDis * Vision.ActiveDis)
                {
                    MovePoint(Target.position);
                    Weapon.Fire();
                }
                else
                {
                    Inspect();
                }
            }
        }

        #endregion
    }
}