using System.ComponentModel.DataAnnotations;

namespace Villa.Models.Dto;

public class VillaDTO
{
    [Required] [MaxLength(30)] 
    
    public int id { get; set; }
    
    public string name { get; set; }
    
    public string Details { get; set; }
    
    public double Rate { get; set; }
    
    public int Sqft { get; set; }
    
    public int occupancy { get; set; }
    
    public string ImageUrl{ get; set; }
    
    public string Amenity { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime UpdateDate { get; set; }
}