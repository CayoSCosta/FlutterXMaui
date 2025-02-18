// main.dart
import 'package:flutter/material.dart';
import 'models/todo.dart';
import 'widgets/todo_list_item.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'To-Do List',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: const MyHomePage(),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key});

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  List<Todo> todos = [];
  List<Todo> removedTodos = [];
  List<Todo> archivedTodos = [];
  String cep = '';
  Map<String, dynamic> cepData = {};
  String errorMessage = '';

  void _addTodo(String description) {
    setState(() {
      todos.add(Todo(description: description));
    });
  }

  void _toggleTodo(int index) {
    setState(() {
      todos[index].isDone = !todos[index].isDone;
    });
  }

  void _removeTodo(int index) {
    setState(() {
      removedTodos.add(todos.removeAt(index));
    });
  }

  void _archiveTodo(int index) {
    setState(() {
      archivedTodos.add(todos.removeAt(index));
    });
  }

  Future<void> _fetchCep(String cep) async {
    setState(() {
      errorMessage = ''; // Limpa mensagens de erro anteriores
      cepData = {}; // Limpa dados de CEP anteriores
    });

    final url = Uri.parse('https://viacep.com.br/ws/$cep/json/');

    try {
      final response = await http.get(url);

      if (response.statusCode == 200) {
        final decodedData = jsonDecode(response.body);

        if (decodedData['erro'] == true) {
          setState(() {
            errorMessage = 'CEP não encontrado.';
          });
        } else {
          setState(() {
            cepData = decodedData;
          });
        }
      } else {
        setState(() {
          errorMessage = 'Erro na requisição: ${response.statusCode}';
        });
      }
    } catch (e) {
      setState(() {
        errorMessage = 'Erro: $e';
      });
    }
  }


  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      length: 4, // Aumentado para 4 abas
      child: Scaffold(
        appBar: AppBar(
          title: const Text('To-Do List'),
          bottom: const TabBar(
            tabs: [
              Tab(text: 'Ativas'),
              Tab(text: 'Excluídas'),
              Tab(text: 'Arquivadas'),
              Tab(text: 'CEP'),
            ],
          ),
        ),
        body: TabBarView(
          children: [
            _buildTodoList(todos),
            _buildTodoList(removedTodos),
            _buildTodoList(archivedTodos),
            _buildCepSearch(), // Widget para a aba de CEP
          ],
        ),
        floatingActionButton: FloatingActionButton(
          onPressed: () {
            showDialog(
              context: context,
              builder: (context) {
                String newTodoDescription = '';
                return AlertDialog(
                  title: const Text('New To-Do'),
                  content: TextField(
                    onChanged: (value) => newTodoDescription = value,
                  ),
                  actions: [
                    TextButton(
                      onPressed: () => Navigator.pop(context),
                      child: const Text('Cancel'),
                    ),
                    TextButton(
                      onPressed: () {
                        if (newTodoDescription.isNotEmpty) {
                          _addTodo(newTodoDescription);
                          Navigator.pop(context);
                        }
                      },
                      child: const Text('Add'),
                    ),
                  ],
                );
              },
            );
          },
          child: const Icon(Icons.add),
        ),
      ),
    );
  }

  Widget _buildTodoList(List<Todo> todos) {
    return ListView.builder(
      itemCount: todos.length,
      itemBuilder: (context, index) {
        return TodoListItem(
          key: Key(todos[index].description),
          todo: todos[index],
          onToggle: () => _toggleTodo(index),
          onRemove: () => _removeTodo(index),
          onArchive: () => _archiveTodo(index),
        );
      },
    );
  }

  Widget _buildCepSearch() {
    return Padding(
      padding: const EdgeInsets.all(16.0),
      child: Column(
        children: [
          TextField(
            keyboardType: TextInputType.number,
            decoration: const InputDecoration(labelText: 'Digite o CEP'),
            onChanged: (value) => cep = value,
          ),
          ElevatedButton(
            onPressed: () => _fetchCep(cep),
            child: const Text('Buscar CEP'),
          ),
          if (errorMessage.isNotEmpty)
            Text(errorMessage, style: TextStyle(color: Colors.red)),
          if (cepData.isNotEmpty)
            Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text('Logradouro: ${cepData['logradouro']}'),
                Text('Bairro: ${cepData['bairro']}'),
                Text('Cidade: ${cepData['localidade']}'),
                Text('Estado: ${cepData['uf']}'),
                // Adicione mais campos conforme necessário
              ],
            ),
        ],
      ),
    );
  }
}