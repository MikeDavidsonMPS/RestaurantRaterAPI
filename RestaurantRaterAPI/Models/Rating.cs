using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class Rating
    {
        //Primary Key
        [Key]
        public int Id { get; set; }

        //Foreign Key
        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }

        //Navigation Property
        public virtual Restaurant Restaurant { get; set; }


        [Required, Range(0, 10)]
        public double FoodScore { get; set; }

        [Required, Range(0, 10)]
        public double CleanlinessScore { get; set; }

        [Required, Range(0, 10)]
        public double EnvironmetScore { get; set; }

        public double AverageRating
        {
            get
            {
                var totalScore = FoodScore = CleanlinessScore = EnvironmetScore;
                return totalScore / 3;
            }
        }



    }
}