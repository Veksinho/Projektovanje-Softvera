using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Komunikacija
{
    public enum Operacija
    {
        PrijaviBroker,

        KreirajBroker,
        PromeniBroker,
        PretraziBroker,
        ObrisiBroker,
        VratiListuBroker,
        VratiListuSviBroker,

        UbaciKategorijaDogadjaja,
        PromeniKategorijaDogadjaja,
        PretraziKategorijaDogadjaja,
        ObrisiKategorijaDogadjaja,
        VratiListuKategorijaDogadjaja,
        VratiListuSviKategorijaDogadjaja,

        UbaciDogadjaj,
        PromeniDogadjaj,
        PretraziDogadjaj,
        ObrisiDogadjaj,
        VratiListuDogadjaj,
        VratiListuSviDogadjaj,

        KreirajKonsignator,
        PromeniKonsignator,
        PretraziKonsignator,
        ObrisiKonsignator,
        VratiListuKonsignator,
        VratiListuSviKonsignator,

        KreirajKarta,
        PromeniKarta,
        PretraziKarta,
        VratiListuKarta,
        VratiListuSviKarta,

        KreirajListing,
        PromeniListing,
        PretraziListing,
        VratiListuListing
    }
}
