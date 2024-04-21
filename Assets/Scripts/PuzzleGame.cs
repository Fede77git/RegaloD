using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGame : MonoBehaviour
{
    [SerializeField] private int difficulty = 4;
    [SerializeField] private Transform gameHolder;
    [SerializeField] private Transform piecePrefab;



    [SerializeField] private List<Texture2D> imageTextures;

    private List<Transform> pieces;
    private Vector2Int dimensions;
    private float width;
    private float height;

    public void StartGame(Texture2D puzzleTexture)
    {
        pieces = new List<Transform>();
        dimensions = GetDimensions(puzzleTexture, difficulty);

        CreatePuzzlePieces(puzzleTexture);
    }


    Vector2Int GetDimensions(Texture2D puzzleTexture, int difficulty)
    {
        Vector2Int dimensions = Vector2Int.zero;
        if (puzzleTexture.width < puzzleTexture.height)
        {
            dimensions.x = difficulty;
            dimensions.y = (difficulty * puzzleTexture.height) / puzzleTexture.width;
        }
        else
        {
            dimensions.x = (difficulty * puzzleTexture.width) / puzzleTexture.height;
            dimensions.y = difficulty;
        }


        return dimensions;
    }

    void CreatePuzzlePieces(Texture2D puzzleTexture)
    {
        height = 1f / dimensions.y;
        float aspect = (float) puzzleTexture.width / puzzleTexture.height;
        width = aspect / dimensions.x;

        for (int row = 0; row < dimensions.y; row++)
        {
            for (int col = 0; col < dimensions.x; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameHolder);
                piece.localPosition = new Vector3(
                    (-width * dimensions.x / 2) + (width * col) + (width / 2),
                    (-height * dimensions.y / 2) + (height * row) + (height / 2),
                    -1);
                piece.localScale = new Vector3(width, height, 1f);

                piece.name = $"Piece {(row * dimensions.x) + col}";
                pieces.Add(piece);
            }
        }


    }
}
