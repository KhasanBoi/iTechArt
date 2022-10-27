﻿using iTechArt.Domain.ModelInterfaces;

namespace iTechArt.Domain.ServiceInterfaces
{
    public interface IMedStaffService
    {
        /// <summary>
        /// Uploads a file of data 
        /// </summary>
        public Task<IMedStaff[]> ImportMedStaffFile();

        /// <summary>
        /// Gets all info from database
        /// </summary>
        public Task<IMedStaff[]> ExportMedStaffFile();
    }
}
