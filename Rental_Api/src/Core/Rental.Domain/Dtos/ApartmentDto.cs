using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;
using Rental.Domain.Enums;

namespace Rental.Domain.Dto
{
    public class ApartmentDto
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public int Floor { get;  set; }
        public int Size { get;  set; }
        public decimal Price { get;  set; }
        public int Rooms { get;  set; }
        public double Latitude { get;  set; }
        public double Longitude { get;  set; }
        public DateTime Created_At { get;  set; }
        public ApartmentStatusEnum Status { get;  set; }
        public string UserName { get;  set; }

         public ApartmentDto()
        {
        }

       
    }
}
