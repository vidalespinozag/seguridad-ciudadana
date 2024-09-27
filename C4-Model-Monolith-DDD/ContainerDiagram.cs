using Structurizr;

namespace C4_Model_Monolith_DDD
{
	public class ContainerDiagram
	{
		private readonly C4 c4;
		private readonly ContextDiagram contextDiagram;

		public Container MobileApplication { get; private set; } = null!;
		public Container ApiRest { get; private set; } = null!;

		public Container Incidents { get; private set; } = null!;
		public Container Users { get; private set; } = null!;
		public Container Notifications { get; private set; } = null!;
		public Container Monitoring { get; private set; } = null!;

		public Container Database { get; private set; } = null!;
		public Container ReplicaDatabase { get; private set; } = null!;
		public Container ReactiveDatabase { get; private set; } = null!;

		public ContainerDiagram(C4 c4, ContextDiagram contextDiagram)
		{
			this.c4 = c4;
			this.contextDiagram = contextDiagram;
		}

		public void Generate() {
			AddContainers();
			AddRelationships();
			ApplyStyles();
			CreateView();
		}

		private void AddContainers()
		{
			MobileApplication = contextDiagram.MonitoringSystem.AddContainer("Aplicacion Movil", "Permite a los usuarios registrar incidentes y realizar consultas sobre la seguridad en diversas zonas.", "Flutter / Dart");

			ApiRest = contextDiagram.MonitoringSystem.AddContainer("API REST", "API REST", "Backend port 8080");
			
			Incidents = contextDiagram.MonitoringSystem.AddContainer("Incidentes BC", "Registro e informacion de Incidentes de Seguridad", "Backend");
			Users = contextDiagram.MonitoringSystem.AddContainer("Ususarios BC", "Información de los usuarios de la aplicacion.", "Backend");
			Notifications = contextDiagram.MonitoringSystem.AddContainer("Notificaciones BC", "Gestion de Notificacion de Incidentes", "Backend");
			Monitoring = contextDiagram.MonitoringSystem.AddContainer("Monitoreo BC", "Monitoreo en tiempo real de los incidentes y servicio de emergencia", "Backend");

			Database = contextDiagram.MonitoringSystem.AddContainer("DB", "", "MySQL");
			ReplicaDatabase = contextDiagram.MonitoringSystem.AddContainer("Replica DB", "", "MySQL");
			ReactiveDatabase = contextDiagram.MonitoringSystem.AddContainer("Reactive DB", "", "Firebase o NOSQL MongoDB");
		}

		private void AddRelationships() {
			contextDiagram.Ciudadano.Uses(MobileApplication, "Consulta");


			MobileApplication.Uses(ApiRest, "API Request", "JSON/HTTPS");


			ApiRest.Uses(Incidents, "API Request", "JSON/HTTPS");
			ApiRest.Uses(Users, "API Request", "JSON/HTTPS");
			ApiRest.Uses(Notifications, "API Request", "JSON/HTTPS");
			ApiRest.Uses(Monitoring, "API Request", "JSON/HTTPS");

			Incidents.Uses(Database, "", "");
			Incidents.Uses(ReplicaDatabase, "", "");

			Users.Uses(Database, "", "");
			Users.Uses(ReplicaDatabase, "", "");

			Notifications.Uses(Database, "", "");
			Notifications.Uses(ReplicaDatabase, "", "");


			Monitoring.Uses(Database, "", "");
			Monitoring.Uses(ReplicaDatabase, "Replica", "");
			Monitoring.Uses(ReactiveDatabase, "", "");

			Monitoring.Uses(contextDiagram.GoogleMaps, "API Request", "JSON/HTTPS");
			Monitoring.Uses(contextDiagram.LocalSecurity, "API Request", "JSON/HTTPS");
		}

		private void ApplyStyles() {
			SetTags();

			Styles styles = c4.ViewSet.Configuration.Styles;

			styles.Add(new ElementStyle(nameof(MobileApplication)) { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
			styles.Add(new ElementStyle(nameof(ApiRest)) { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });

			styles.Add(new ElementStyle(nameof(Users)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(Notifications)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(Incidents)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
			styles.Add(new ElementStyle(nameof(Monitoring)) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });

			styles.Add(new ElementStyle(nameof(Database)) { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
			styles.Add(new ElementStyle(nameof(ReplicaDatabase)) { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
			styles.Add(new ElementStyle(nameof(ReactiveDatabase)) { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
		}

		private void SetTags()
		{
			MobileApplication.AddTags(nameof(MobileApplication));
			ApiRest.AddTags(nameof(ApiRest));

			Users.AddTags(nameof(Users));
			Notifications.AddTags(nameof(Notifications));
			Incidents.AddTags(nameof(Incidents));
			Monitoring.AddTags(nameof(Monitoring));

			Database.AddTags(nameof(Database));
			ReplicaDatabase.AddTags(nameof(ReplicaDatabase));
			ReactiveDatabase.AddTags(nameof(ReactiveDatabase));
		}

		private void CreateView() {
			ContainerView containerView = c4.ViewSet.CreateContainerView(contextDiagram.MonitoringSystem, "Contenedor", "Diagrama de contenedores");
			containerView.AddAllElements();
			containerView.DisableAutomaticLayout();
		}
	}
}