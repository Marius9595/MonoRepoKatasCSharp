namespace MarsRovers.lib;

public class MarsRoverNavigator
{
    private SpatialSituation _spatialSituation;
    private readonly ObstacleDetector _obstacleDetector;
    private readonly Satellite _satellite;
    public MarsRoverNavigator(ObstacleDetector obstacleDetector, Satellite satellite)
    {
        this._obstacleDetector = obstacleDetector;
        this._satellite = satellite;
        this._spatialSituation = new SpatialSituation(new Coordinates(0, 0), Direction.North);
    }
    public  SpatialSituation spatialSituation()
    {
        return _spatialSituation;
    }

    public void executeCommands(Commands commands)
    {
        var newSpatialSituation = commands.executeWith(_spatialSituation);
        if (_obstacleDetector.isThereAnObstacleAt(newSpatialSituation.Coordinates))
        {
            
        }
        if (_satellite.isExceedingTheBoundaries(newSpatialSituation.Coordinates))
        {
            
        }

        this._spatialSituation = newSpatialSituation;
    }
}