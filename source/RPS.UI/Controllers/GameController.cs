using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using RPS.UI.Views;

namespace RPS.UI.Controllers
{
    public class GameController
    {
        private IRegionManager _regionManager;

        public GameController(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void ShowRegisterView()
        {
            var region = _regionManager.Regions[RegionNames.MainRegion];

            var registerView = region.GetView("RegisterView");
            if (registerView == null)
            {
                registerView = ServiceLocator.Current.GetInstance<RegisterView>();
                region.Add(registerView, "RegisterView");
            }
        }
        public void CloseRegisterView()
        {
            var region = _regionManager.Regions[RegionNames.MainRegion];

            var registerView = region.GetView("RegisterView");
            if (registerView != null)
            {
                region.Remove(registerView);
            }
        }

        public void ShowTournamentView()
        {
            var region = _regionManager.Regions[RegionNames.MainRegion];

            var tournamentView = region.GetView("TournamentView");
            if (tournamentView == null)
            {
                tournamentView = ServiceLocator.Current.GetInstance<TournamentView>();
                region.Add(tournamentView, "TournamentView");
            }
        }
        public void CloseTournamentView()
        {
            var region = _regionManager.Regions[RegionNames.MainRegion];

            var tournamentView = region.GetView("TournamentView");
            if (tournamentView != null)
            {
                region.Remove(tournamentView);
            }
        }

        public void ShowCreateTournamentView()
        {
            var region = _regionManager.Regions[RegionNames.MainRegion];

            var createTournamentView = region.GetView("CreateTournamentView");
            if (createTournamentView == null)
            {
                createTournamentView = ServiceLocator.Current.GetInstance<CreateTournamentView>();
                region.Add(createTournamentView, "CreateTournamentView");
            }
        }
        public void CloseCreateTournamentView()
        {
            var region = _regionManager.Regions[RegionNames.MainRegion];

            var createTournamentView = region.GetView("CreateTournamentView");
            if (createTournamentView != null)
            {
                region.Remove(createTournamentView);
            }
        }
    }
}
