namespace RepositoryProject.Utilities
{
    public class GenericResponse<T> 
    { 
        public bool State { get; set; }
        public string? Message { get; set; }
        public T? Object { get; set; }
        public List<T>? ListObject { get; set; }
    }
}

