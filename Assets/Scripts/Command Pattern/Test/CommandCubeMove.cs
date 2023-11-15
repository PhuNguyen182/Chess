using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommandPattern;

public class CommandCubeMove : MonoBehaviour
{
    private MoveCommand _moveCommand;
    private Vector3 _startPosition, _endPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _startPosition = transform.position;
            _endPosition = _startPosition + Vector3.forward;
            _moveCommand = new MoveCommand(transform, _startPosition, _endPosition);
            CommandManager.AddCommand(_moveCommand);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _startPosition = transform.position;
            _endPosition = _startPosition + Vector3.back;
            _moveCommand = new MoveCommand(transform, _startPosition, _endPosition);
            CommandManager.AddCommand(_moveCommand);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _startPosition = transform.position;
            _endPosition = _startPosition + Vector3.right;
            _moveCommand = new MoveCommand(transform, _startPosition, _endPosition);
            CommandManager.AddCommand(_moveCommand);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _startPosition = transform.position;
            _endPosition = _startPosition + Vector3.left;
            _moveCommand = new MoveCommand(transform, _startPosition, _endPosition);
            CommandManager.AddCommand(_moveCommand);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Undo
            CommandManager.Undo();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            // Redo
            CommandManager.Redo();
        }
    }
}
