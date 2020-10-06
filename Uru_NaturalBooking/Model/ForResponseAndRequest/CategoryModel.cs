using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ForResponseAndRequest
{
    public class CategoryModel : ModelBaseForRequestAndResponse<Category, CategoryModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CategoryModel() { }

        public override Category ToEntity() => new Category()
        {
            Id = Id, 
            Name = Name
        };

        protected override CategoryModel SetModel(Category category)
        {
            Id = category.Id; 
            Name = category.Name;
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is CategoryModel model &&
                   Name == model.Name;
        }
    }
}
