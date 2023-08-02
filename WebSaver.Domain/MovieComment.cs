using Kirel.Repositories.Interfaces;

namespace Domain;

public class MovieComment : ICreatedAtTrackedEntity, IKeyEntity<int>
{
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
}