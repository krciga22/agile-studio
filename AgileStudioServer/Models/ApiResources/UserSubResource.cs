﻿using AgileStudioServer.Models.Entities;

namespace AgileStudioServer.Models.ApiResources
{
    public class UserSubResource
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserSubResource(User user)
        {
            ID = user.ID;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
    }
}
