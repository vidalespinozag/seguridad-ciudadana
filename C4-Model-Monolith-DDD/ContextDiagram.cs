using Structurizr;

namespace C4_Model_Monolith_DDD
{
	public class ContextDiagram
	{
		private readonly C4 c4;

		public SoftwareSystem MonitoringSystem { get; private set; } = null!;
		public SoftwareSystem GoogleMaps { get; private set; } = null!;
		public SoftwareSystem LocalSecurity { get; private set; } = null!;
        public SoftwareSystem Notification { get; private set; } = null!;
        public Person Ciudadano { get; private set; } = null!;

		public ContextDiagram(C4 c4)
		{
			this.c4 = c4;
		}

		public void Generate() {
			AddSoftwareSystems();
			AddPeople();
			AddRelationships();
			ApplyStyles();
			CreateView();
		}

		private void AddSoftwareSystems()
		{
			MonitoringSystem = c4.Model.AddSoftwareSystem("Aplicación de Seguridad Ciudadana", "Aplicacion movil que registra y muestra incidentes en tiempo real.");
			GoogleMaps = c4.Model.AddSoftwareSystem("Google Maps", "Plataforma que ofrece una REST API para le geolocalizacion de los usuairos y reporte de incidencias.");
			LocalSecurity = c4.Model.AddSoftwareSystem("Servicio de Emergencia", "Permite transmitir información en tiempo real algun incidente que requiera emergencia.");
			Notification = c4.Model.AddSoftwareSystem("Servicio de Notificaciones", "Permite notificar a los usuarios sobre incidentes registrados.");
		}

		private void AddPeople()
		{
			Ciudadano = c4.Model.AddPerson("Ciudadano", "Ciudadano peruano mayor de 18 años.");
		}

		private void AddRelationships() {
			Ciudadano.Uses(MonitoringSystem, "Registra y Realiza consultas de los incidentes ocurridos en un radio de 5km.");

			MonitoringSystem.Uses(LocalSecurity, "Consulta información en tiempo real sobre algun servicio de emergencia.");
			MonitoringSystem.Uses(GoogleMaps, "Usa");
			MonitoringSystem.Uses(Notification, "Usar");
		}

		private void ApplyStyles() {
			SetTags();

			Styles styles = c4.ViewSet.Configuration.Styles;
			
			styles.Add(new ElementStyle(nameof(MonitoringSystem)) { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(GoogleMaps)) { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(LocalSecurity)) { Background = "#2f95c7", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(Notification)) { Background = "#22c3c7", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(Ciudadano)) { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
		}

		private void SetTags()
		{
			MonitoringSystem.AddTags(nameof(MonitoringSystem));
			GoogleMaps.AddTags(nameof(GoogleMaps));
			LocalSecurity.AddTags(nameof(LocalSecurity));
			Notification.AddTags(nameof(Notification));
			Ciudadano.AddTags(nameof(Ciudadano));
		}

		private void CreateView() {
			SystemContextView contextView = c4.ViewSet.CreateSystemContextView(MonitoringSystem, "Contexto", "Diagrama de contexto");
			contextView.AddAllSoftwareSystems();
			contextView.AddAllPeople();
			contextView.DisableAutomaticLayout();
		}
	}
}