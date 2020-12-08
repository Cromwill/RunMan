using System.Collections;

public interface IEnemySpawnGenerator
{
    ITile[] GetTilesToSpawn(ITile[] tiles, ITile currentTile);
    ITile GetTileForEnemy();

}
