﻿using Microsoft.AspNetCore.Mvc;
using NetCoreSeguridadDoctores.Filters;
using NetCoreSeguridadDoctores.Models;
using NetCoreSeguridadDoctores.Repositories;
using System.Collections.Specialized;
using System.Security.Claims;

namespace NetCoreSeguridadDoctores.Controllers
{
    public class DoctoresController : Controller
    {
        private RepositoryHospital repo;

        public DoctoresController(RepositoryHospital repo)
        {
            this.repo = repo;
        }

        [AuthorizeDoctores]
        public async Task<IActionResult> PerfilDoctor()
        {
            string data =
                HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Doctor doctor =
                await this.repo.FindDoctorAsync(int.Parse(data));
            return View(doctor);
        }

        public async Task<IActionResult> Enfermos()
        {
            List<Enfermo> enfermos =
                await this.repo.GetEnfemosAsync();
            return View(enfermos);
        }

        [AuthorizeDoctores]
        public async Task<IActionResult> DeleteEnfermo(int id)
        {
            Enfermo enfermo =
                await this.repo.FindEnfermoAsync(id);
            return View(enfermo);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Enfermo enfermo =
                await this.repo.FindEnfermoAsync(id);
            return View(enfermo);
        }

        [AuthorizeDoctores]
        [HttpPost]
        [ActionName("DeleteEnfermo")]
        public async Task<IActionResult> EliminarEnfermo(int id)
        {
            await this.repo.DeleteEnfermoAsync(id);
            return RedirectToAction("Enfermos");
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSick(int id)
        {
            await this.repo.DeleteEnfermoAsync(id);
            return RedirectToAction("Enfermos");
        }
    }
}
