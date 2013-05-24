using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abstracts;
using Abstracts.PersistenceDtos;
using Raven.Client;

namespace DemoWebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDocumentSession _session;

        public CustomerController(ICustomerRepository customerRepository, IDocumentSession session)
        {
            if (customerRepository == null) throw new ArgumentNullException("customerRepository");
            if (session == null) throw new ArgumentNullException("session");
            _customerRepository = customerRepository;
            _session = session;
        }

        //
        // GET: /Customer/

        const int PageSize = 25;
        public ActionResult Index(int page = 1)
        {
            var take = PageSize;
            var skip = (page - 1)*PageSize;

            var allCustomers = _customerRepository.List(skip, take);

            return View(allCustomers);
        }

        //
        // GET: /Customer/Details/5

        public ActionResult Details(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var model = AutoMapper.Mapper.Map<Customer, CustomerDisplayModel>(customer);
            return View(model);
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            return View(new CustomerUpdateModel());
        }

        //
        // POST: /Customer/Create

        [HttpPost]
        public ActionResult Create(CustomerUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                    {
                        Email = model.Email,
                        FirstName = model.FirstName,
                        Name = model.Name,
                    };

                _customerRepository.Add(customer);
                _session.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);

        }

        //
        // GET: /Customer/Edit/5

        public ActionResult Edit(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var model = AutoMapper.Mapper.Map<Customer, CustomerUpdateModel>(customer);
            return View(model);
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, CustomerUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerRepository.GetById(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }

                customer.Email = model.Email;
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.Name = model.LastName;

                _session.SaveChanges();
                return RedirectToAction("Details", new {id = id});
            }
            return View(model);
        }

        //
        // GET: /Customer/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }



    public class CustomerBaseModel
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class CustomerDisplayModel : CustomerBaseModel
    {
        public CustomerDisplayModel()
        {
            Addresses = new List<AddressDisplayModel>();
        }

        public List<AddressDisplayModel> Addresses { get; private set; }
    }

    public class CustomerUpdateModel : CustomerBaseModel
    {
        
    }

    public class AddressDisplayModel
    {
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public AddressType AddressType { get; set; }
    }
}
