﻿using Core.Business;
using Core.Databases;
using Core.Models;

namespace Core.ViewModels
{
    public class PaymentsViewModel : BaseCollectionViewModel<Payment, PaymentBusiness, PaymentsManager>
    { }
}
