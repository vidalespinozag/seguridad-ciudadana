using Structurizr;
using Structurizr.Api;

namespace C4_Model_Monolith_DDD
{
	public class C4
	{
        private readonly long workspaceId = 91857;
		private readonly string apiKey = "c38e7014-2f41-40e0-ab63-75f6b5cd8a97";
		private readonly string apiSecret = "f846804c-9773-45e3-b67f-fc0388a35817";

		public StructurizrClient StructurizrClient { get; }
		public Workspace Workspace { get; }
		public Model Model { get; }
		public ViewSet ViewSet { get; }

		public C4()
		{
			string workspaceName = "Aplicación de Seguridad Ciudadana";
			string workspaceDescription = "Sistema de alertas en tiempo real para la seguridad ciudadana.";
			StructurizrClient = new StructurizrClient(apiKey, apiSecret);
			Workspace = new Workspace(workspaceName, workspaceDescription);
			Model = Workspace.Model;
			ViewSet = Workspace.Views;
		}

		public void Generate() {
			ContextDiagram contextDiagram = new(this);
			ContainerDiagram containerDiagram = new(this, contextDiagram);
			MonitoringComponentDiagram monitoringComponentDiagram = new(this, contextDiagram, containerDiagram);
			contextDiagram.Generate();
			containerDiagram.Generate();
			monitoringComponentDiagram.Generate();
			PutWorkspace();
		}

		private void PutWorkspace() {
			StructurizrClient.UnlockWorkspace(workspaceId);
			StructurizrClient.PutWorkspace(workspaceId, Workspace);
		}
	}
}