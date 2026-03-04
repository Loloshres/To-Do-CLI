using System.Text.Json;
using System.IO;

TaskManager mg = new TaskManager();
mg.LoadJSON();

ToDo toDo = new ToDo();

toDo.Interaction(mg);


class ToDo
{
    public void Interaction(TaskManager manager)
    {
        while(true)
        {
            int methodNum;
            int idOfTask;
            string nameOfTask;

            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Удалить задачу");
            Console.WriteLine("3. Показать задачи");
            Console.WriteLine("4. Выполнить задачу");
            Console.WriteLine("0. Выход");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Введите номер команды:");
            methodNum = int.Parse(Console.ReadLine());
            switch (methodNum)
            {
                case 0:
                    Console.WriteLine("Программа завершила работу!");
                    return;

                case 1:
                    Console.WriteLine("Введите вашу задачу:");
                    nameOfTask = Console.ReadLine();
                    manager.AddTask(nameOfTask);
                    break;
                case 2:
                    Console.WriteLine("Введите Id вашей задачи, которую хотите удалить");
                    idOfTask = int.Parse(Console.ReadLine());
                    manager.RemoveTask(idOfTask);
                    break;
                case 3:
                    manager.ShowTasks();
                    break;
                case 4:
                    Console.WriteLine("Введите Id вашей задачи, которую выполнить");
                    idOfTask = int.Parse(Console.ReadLine());
                    manager.SetComplete(idOfTask);
                    break;
                default:
                    Console.WriteLine("Неверная комманда! Попробуйте ещё раз");
                    break;
            }
            Console.WriteLine("Нажмите на ENTER");                                                                                                                                                                                    
            Console.ReadLine();
            Console.WriteLine("---------------------------------------");
        }
    }
}


public class TaskItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsCompleted { get; set; }

    public TaskItem(int id, string name) {Id = id; Name = name;}

    public void MarkAsCompleted()
    {
        if(!IsCompleted) {IsCompleted = true;}
        else {return;}
        Console.WriteLine("Задача выполнена");
    }
}


public class TaskManager
{
    public List<TaskItem> Tasks { get; private set;} = new List<TaskItem>();
    private int _nextId = 1;
    public void AddTask(string name)
    {
        if(name != null)
        {
            Tasks.Add(new TaskItem(_nextId, name));
            Console.WriteLine("Задача добавлена");
           _nextId++;
           SaveToJSON();
        }
    }

    public void RemoveTask(int id)
    {
        Tasks.RemoveAll(task => task.Id == id);
        Console.WriteLine($"Задача под номером {id} убрана");
        SaveToJSON();
    }    
    public void SetComplete(int id)
    {
        foreach(var task in Tasks)
        {
            if(task.Id == id) {task.MarkAsCompleted();}
        }
        SaveToJSON();
    }
    public void ShowTasks()
    {
        foreach(var task in Tasks)
        {
            Console.WriteLine($"{task.Id}. {task.Name} | Статус: {task.IsCompleted}");
        }
    }

    public void SaveToJSON()
    {
        string json = JsonSerializer.Serialize(Tasks);
        File.WriteAllText("task.json", json);
    }

    public void LoadJSON()
    {
        if (File.Exists("task.json"))
        {
            string json = File.ReadAllText("task.json");
            Tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();

            if (Tasks.Count > 0)
            {
                _nextId = Tasks.Max(task => task.Id) + 1;
            }
        }
    }
}

