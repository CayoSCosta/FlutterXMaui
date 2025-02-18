import 'package:flutter/material.dart';
import '../models/todo.dart';

class TodoListItem extends StatelessWidget {
  final Todo todo;
  final VoidCallback onToggle;
  final VoidCallback onRemove;
  final VoidCallback onArchive;

  const TodoListItem({
    super.key,
    required this.todo,
    required this.onToggle,
    required this.onRemove,
    required this.onArchive,
  });

  @override
  Widget build(BuildContext context) {
    return Dismissible(
      key: Key(todo.description),
      onDismissed: (direction) {
        if (direction == DismissDirection.startToEnd) {
          onRemove();
        } else {
          onArchive();
        }
      },
      background: Container(
        color: Colors.red,
        alignment: Alignment.centerLeft,
        padding: const EdgeInsets.only(left: 16.0),
        child: const Icon(Icons.delete, color: Colors.white),
      ),
      secondaryBackground: Container(
        color: Colors.yellow,
        alignment: Alignment.centerRight,
        padding: const EdgeInsets.only(right: 16.0),
        child: const Icon(Icons.archive, color: Colors.white),
      ),
      child: ListTile(
        title: Text(
          todo.description,
          style: TextStyle(
            decoration: todo.isDone ? TextDecoration.lineThrough : null,
          ),
        ),
        leading: Checkbox(
          value: todo.isDone,
          onChanged: (value) => onToggle(),
        ),
      ),
    );
  }
}