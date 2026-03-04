TaskManager mg = new TaskManager();
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
                    nameOfTask = Console.ReadLine();
                    manager.AddTask(nameOfTask);
                    break;
                case 2:
                    idOfTask = int.Parse(Console.ReadLine());
                    manager.RemoveTask(idOfTask);
                    break;
                case 3:
                    manager.ShowTasks();
                    break;
                case 4:
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
    public int Id { get;}
    public string Name { get; set; }
    public bool IsCompleted { get; private set; }

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
    public List<TaskItem> Tasks { get; } = new List<TaskItem>();
    private int _nextId = 1;
    public void AddTask(string name)
    {
        if(name != null)
        {
            Tasks.Add(new TaskItem(_nextId, name));
            Console.WriteLine("Задача добавлена");
           _nextId++;
        }
    }

    public void RemoveTask(int id)
    {
        Tasks.RemoveAll(task => task.Id == id);
        Console.WriteLine($"Задача под номером {id} убрана");
    }

    public void SetComplete(int id)
    {
        foreach(var task in Tasks)
        {
            if(task.Id == id) {task.MarkAsCompleted();}
        }
    }
    public void ShowTasks()
    {
        foreach(var task in Tasks)
        {
            Console.WriteLine($"{task.Id}. {task.Name} | Статус: {task.IsCompleted}");
        }
    }
}
