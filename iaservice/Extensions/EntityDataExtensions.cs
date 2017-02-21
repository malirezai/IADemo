using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iaservice.Extensions
{
    public static class EntityDataExtensions
    {
        public static void UpdateEntityData(this EntityData entityToBeUpdated, EntityData updateToBeApplied)
        {
            entityToBeUpdated.CreatedAt = updateToBeApplied.CreatedAt;
            entityToBeUpdated.UpdatedAt = updateToBeApplied.UpdatedAt;
            entityToBeUpdated.Version = updateToBeApplied.Version;
            entityToBeUpdated.Deleted = updateToBeApplied.Deleted;
        }
    }
}