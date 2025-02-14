using System.Collections.Generic;
using UnityEngine;

public static class GridStaticSelectors {
    public static List<Vector2Int> GetPositions(Selector selector, Vector2Int startPos) {
        List<Vector2Int> result = new();

        switch (selector.type) {
            case SelectorType.SingleTile:
                result.Add(startPos);
                break;
            case SelectorType.Circle:
                result = GetAvailableTilesCircle(selector, startPos);
                break;
            case SelectorType.Line:
                result = GetAvailableTilesLine(selector, startPos);
                break;

            case SelectorType.FriendlyUnits:
            case SelectorType.EnemyUnits:
            case SelectorType.AllUnits:
                GetAvailableTilesUnits(selector, startPos);
                break;

            default:
                Debug.LogError($"{selector.type} not yet Implemented");
                break;
        }

        return result;
    }

    private static List<Vector2Int> GetAvailableTilesCircle(Selector selector, Vector2Int startPos) {
        List<Vector2Int> result = new();

        GridStaticFunctions.RippleThroughSquareGridPositions(startPos, selector.range, (currentPos, index) =>
        {
            result.Add(currentPos);
        });

        if (!selector.includeCentreTile)
            result.Remove(startPos);

        return result;
    }

    private static List<Vector2Int> GetAvailableTilesLine(Selector selector, Vector2Int startPos) {
        List<Vector2Int> result = new() {
            startPos
        };

        if (selector.AllDirections)
            for (int j = 0; j < 6; j++) {
                result.Add(startPos);

                for (int i = 0; i < selector.range; i++) {
                    if (GridStaticFunctions.TryGetHexNeighbour(result[^1], j, out Vector2Int pos))
                        result.Add(pos);
                }

                result.Remove(startPos);
            }
        else
            for (int i = 0; i < selector.range; i++) {
                if (GridStaticFunctions.TryGetHexNeighbour(result[^1], selector.rotIndex, out Vector2Int pos))
                    result.Add(pos);
            }

        if (!selector.includeCentreTile)
            result.Remove(startPos);

        return result;
    }

    private static List<Vector2Int> GetAvailableTilesUnits(Selector selector, Vector2Int startpos) {
        List<Vector2Int> result = new();

        if (selector.includeCentreTile)
            result.Add(startpos);

        switch (selector.type) {
            case SelectorType.AllUnits:
                return new List<Vector2Int>();
            case SelectorType.FriendlyUnits:
                return new List<Vector2Int>();
            case SelectorType.EnemyUnits:
                return new List<Vector2Int>();

            default:
                return null;
        }
    }

    private static List<Vector2Int> GetAllTiles(Selector selector, Vector2Int startPos) {
        List<Vector2Int> result = new();

        switch (selector.type) {
            case SelectorType.AllUnits:
                return new List<Vector2Int>();
            case SelectorType.FriendlyUnits:
                return new List<Vector2Int>();
            case SelectorType.EnemyUnits:
                return new List<Vector2Int>();

            default:
                return null;
        }
    }
}
