using System;


namespace GeekBrainsFPS
{
    public interface IPointsGiver
    {
        event Action<IPointsGiver> OnPointChange;

        int GivePoints();
    }
}