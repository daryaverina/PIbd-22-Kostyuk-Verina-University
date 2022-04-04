﻿namespace UniversityContracts.BindingModels
{
    public class GroupBindingModel
    {
        public int? Id { get; set; }
        public string Speciality { get; set; }
        public int SemestersAmount { get; set; }
        public DateTime DateCreated { get; set; }
        public int? CustomerID { get; set; }
    }
}
