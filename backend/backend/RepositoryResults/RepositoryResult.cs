namespace backend.RepositoryResults
{
    public class RepositoryResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }

        public RepositoryResult(bool Success, string Message = null, List<T> Data = default)
        {
            this.Success = Success;
            this.Message = Message;
            this.Data = Data;
        }

        public RepositoryResult(bool Success, string Message = null, T Data = default)
        {
            this.Success = Success;
            this.Message = Message;
            this.Data = new List<T> { Data };
        }
    }
}
