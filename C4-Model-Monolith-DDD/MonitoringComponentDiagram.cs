using Structurizr;

namespace C4_Model_Monolith_DDD
{
	public class MonitoringComponentDiagram
	{
		private readonly C4 c4;
		private readonly ContextDiagram contextDiagram;
		private readonly ContainerDiagram containerDiagram;

		public Component DomainLayer { get; private set; } = null!;

		public Component MonitoringController { get; private set; } = null!;

		public Component MonitoringApplicationService { get; private set; } = null!;

		public Component FlightRepository { get; private set; } = null!;
		public Component LocationRepository { get; private set; } = null!;
		public Component VaccineLoteRepository { get; private set; } = null!;

		public Component AircraftSystemFacade { get; private set; } = null!;

		public MonitoringComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
		{
			this.c4 = c4;
			this.contextDiagram = contextDiagram;
			this.containerDiagram = containerDiagram;
		}

		public void Generate() {
			AddComponents();
			AddRelationships();
			ApplyStyles();
			CreateView();
		}

		private void AddComponents()
		{
			DomainLayer = containerDiagram.Monitoring.AddComponent("Domain Layer", "", "C#");

			MonitoringController = containerDiagram.Monitoring.AddComponent("Monitoring Controller", "REST API endpoints de monitoreo.", "ASPNET Core REST Controller");

			MonitoringApplicationService = containerDiagram.Monitoring.AddComponent("Monitoring Application Service", "Provee métodos de monitoreo", "C#");

			FlightRepository = containerDiagram.Monitoring.AddComponent("Flight Repository", "", "C#");
			VaccineLoteRepository = containerDiagram.Monitoring.AddComponent("VaccineLote Repository", "", "C#");
			LocationRepository = containerDiagram.Monitoring.AddComponent("Location Repository", "", "C#");

			AircraftSystemFacade = containerDiagram.Monitoring.AddComponent("Aircraft System Facade", "", "C#");
		}

		private void AddRelationships() {
			containerDiagram.ApiRest.Uses(MonitoringController, "", "JSON/HTTPS");

			MonitoringController.Uses(MonitoringApplicationService, "Usa");
			MonitoringController.Uses(AircraftSystemFacade, "Usa");

			MonitoringApplicationService.Uses(DomainLayer, "Usa", "");
			MonitoringApplicationService.Uses(FlightRepository, "Usa", "");
			MonitoringApplicationService.Uses(VaccineLoteRepository, "Usa", "");
			MonitoringApplicationService.Uses(LocationRepository, "Usa", "");

			FlightRepository.Uses(containerDiagram.Database, "Usa", "");
			FlightRepository.Uses(containerDiagram.ReplicaDatabase, "Usa", "");
			VaccineLoteRepository.Uses(containerDiagram.Database, "Usa", "");
			VaccineLoteRepository.Uses(containerDiagram.ReplicaDatabase, "Usa", "");
			LocationRepository.Uses(containerDiagram.Database, "Usa", "");
			LocationRepository.Uses(containerDiagram.ReplicaDatabase, "Usa", "");
			LocationRepository.Uses(containerDiagram.ReactiveDatabase, "Usa", "");
			LocationRepository.Uses(contextDiagram.GoogleMaps, "", "JSON/HTTPS");

			AircraftSystemFacade.Uses(contextDiagram.LocalSecurity, "JSON/HTTPS");
		}

		private void ApplyStyles() {
			SetTags();

			Styles styles = c4.ViewSet.Configuration.Styles;

			styles.Add(new ElementStyle(nameof(DomainLayer)) { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

			styles.Add(new ElementStyle(nameof(MonitoringController)) { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

			styles.Add(new ElementStyle(nameof(MonitoringApplicationService)) { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

			styles.Add(new ElementStyle(nameof(FlightRepository)) { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(LocationRepository)) { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(VaccineLoteRepository)) { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

			styles.Add(new ElementStyle(nameof(AircraftSystemFacade)) { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
		}

		private void SetTags()
		{
			DomainLayer.AddTags(nameof(DomainLayer));

			MonitoringController.AddTags(nameof(MonitoringController));

			MonitoringApplicationService.AddTags(nameof(MonitoringApplicationService));

			FlightRepository.AddTags(nameof(FlightRepository));
			LocationRepository.AddTags(nameof(LocationRepository));
			VaccineLoteRepository.AddTags(nameof(VaccineLoteRepository));

			AircraftSystemFacade.AddTags(nameof(AircraftSystemFacade));
		}

		private void CreateView() {
			ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.Monitoring, "Components", "Component Diagram");
			componentView.Add(containerDiagram.MobileApplication);
			componentView.Add(containerDiagram.ApiRest);
			componentView.Add(containerDiagram.Database);
			componentView.Add(containerDiagram.ReplicaDatabase);
			componentView.Add(containerDiagram.ReactiveDatabase);
			componentView.Add(contextDiagram.LocalSecurity);
			componentView.Add(contextDiagram.GoogleMaps);
			componentView.AddAllComponents();
			componentView.DisableAutomaticLayout();
		}
	}
}