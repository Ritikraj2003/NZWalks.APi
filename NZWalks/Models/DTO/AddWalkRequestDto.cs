using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [MinLength(2,ErrorMessage ="Name should be More than 2 Letter")]
        [MaxLength(50, ErrorMessage = "Name has to be maximum of 50 character")]
        public string Name { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Name should be More than 10 Letter")]
        [MaxLength(200, ErrorMessage = "Name has to be maximum of 200 character")]
        public string Description { get; set; }
    
        [Required]
        [MinLength(2, ErrorMessage = "Name should be More than 2 Letter")]
        [MaxLength(10, ErrorMessage = "Name has to be maximum of  character")]
        public double lengthInKm {  get; set; }
     
        public string? WalkImageUrl {  get; set; }
        [Required]
        public Guid RegionId { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }

    }
}
