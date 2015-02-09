using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRoom.Model
{
	[Table("ROOM_TYPES")]
	[JsonObject(IsReference = true)]
	public partial class RoomType
	{	
		[Key]
		[Column("Id")]
		public int RoomTypeId { get; set; }

		
		[Column("IdHotel")]        
		public int HotelId { get; set; }

		[Required]
		[StringLength(150)]
		public string Name { get; set; }

		public int IdTranslationName { get; set; }
	  
		public virtual Translation Translation { get; set; }

	}
}
