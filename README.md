# 💻 InsightFlow – Workspace Service

Servicio independiente (Microservicio) encargado de gestionar los **Workspaces** (espacios de trabajo) del proyecto InsightFlow.

**Características Principales:**

  * **CRUD Completo:** Creación, visualización, edición y eliminación lógica (*Soft Delete*) de workspaces.
  * **Persistencia:** Datos **en memoria** (para este taller).
  * **DevOps:** Desplegado continuo mediante **CI/CD** (GitHub Actions a Render).
  * **Integración:** Subida de íconos a **Cloudinary**.

-----

## 🚀 Tecnologías Utilizadas

  * **Backend:** .NET 9.0 (ASP.NET Core Web API)
  * **Contenerización:** Docker
  * **Plataforma de Despliegue:** Render
  * **CI/CD:** GitHub Actions
  * **Almacenamiento de Íconos:** Cloudinary
  * **Pruebas (Opcional):** Postman

-----

## 🛠️ Configuración y Ejecución Local (Docker / .NET)

### 1\. Clonar el Repositorio

```bash
git clone https://github.com/<tu-usuario>/insightflow-workspace-service.git
cd insightflow-workspace-service/WorkspaceService
```

### 2\. Configuración de Credenciales (Cloudinary)

El servicio requiere tus credenciales de **Cloudinary** para el manejo de íconos.

1.  Copia el archivo de configuración de desarrollo a producción local:

    ```bash
    cp appsettings.Development.json appsettings.json
    ```

2.  Edita el nuevo archivo **`appsettings.json`** e inserta tus credenciales:

    ```json
    {
      "Logging": { ... },
      "Cloudinary": {
        "CloudName": "TU_CLOUD_NAME",
        "ApiKey": "TU_API_KEY",
        "ApiSecret": "TU_API_SECRET"
      },
      "AllowedHosts": "*"
    }
    ```

    *Asegúrate de reemplazar `TU_CLOUD_NAME`, `TU_API_KEY` y `TU_API_SECRET` con tus valores reales.*

### 3\. Ejecutar con Docker (Recomendado)

```bash
# a) Construir la imagen
docker build -t insightflow-workspace-service .

# b) Ejecutar el contenedor
docker run -p 8080:8080 insightflow-workspace-service
```

El servicio estará disponible en: `http://localhost:8080/api/workspaces`

### 4\. Ejecutar Directamente con .NET (Opcional)

Si no usas Docker, puedes ejecutar el servicio en modo desarrollo:

```bash
# Restaurar dependencias y ejecutar
dotnet run
```

El servicio estará disponible en: `http://localhost:8080`

-----

## 📌 Datos Iniciales (Seeder)

El servicio utiliza IDs de usuario fijos para cargar dos workspaces de ejemplo al inicio:

  * **`userA` (OwnerId):** `11111111-1111-1111-1111-111111111111`
  * **`userB` (OwnerId/Miembro):** `22222222-2222-2222-2222-222222222222`

| Workspace | Nombre | Propietario | Descripción | Miembros Adicionales |
| :---: | :--- | :--- | :--- | :--- |
| **1** | Proyecto Arquitectura | userA | Workspace para el proyecto de arquitectura. | userB (Rol: Editor) |
| **2** | Investigación TI | userB | Workspace de investigación sobre microservicios. | Ninguno |

*Nota: Los IDs de los Workspaces son generados automáticamente al iniciar el servicio (UUID V4). Debes consultarlos con el endpoint `GET /`.*

-----

## 📡 Endpoints Principales de la API

* **URL Base (Render):** `https://insightflow-workspace-service-fh1q.onrender.com/api/workspaces`
* **URL Base (Local):** `http://localhost:8080/api/workspaces`

| Método | Ruta (Path Relativa) | Descripción | Requerimientos |
| :--- | :--- | :--- | :--- |
| **GET** | `/` | Obtiene la lista de todos los workspaces. | Ninguno. |
| **GET** | `/user/{userId}` | Obtiene todos los workspaces de los que el usuario es **miembro**. | UUID de usuario. |
| **GET** | `/{workspaceId}` | Obtiene los detalles de un workspace específico. | UUID de workspace. |
| **POST** | `/` | **Crea** un nuevo workspace. | Body JSON (WorkspaceCreationDTO) y Icono. |
| **PUT** | `/{workspaceId}?requesterId={userId}` | **Actualiza** un workspace existente. | UUID de workspace y Query Param `requesterId`. |
| **DELETE** | `/{workspaceId}?requesterId={userId}` | Realiza un **Soft Delete** del workspace. | UUID de workspace y Query Param `requesterId` (Solo rol Propietario). |

### Ejemplos de Uso (Render)

| Endpoint | Ejemplo |
| :--- | :--- |
| **GET by User** | `.../api/workspaces/user/11111111-1111-1111-1111-111111111111` |
| **GET by ID** | `.../api/workspaces/7f18091a-e78b-4c6a-acad-79881de4fb01` |
| **PUT/EDIT** | `.../api/workspaces/7f18091a-e78b-4c6a-acad-79881de4fb01?requesterId=22222222-2222-2222-2222-222222222222` |
| **DELETE/SOFT DELETE** | `.../api/workspaces/7f18091a-e78b-4c6a-acad-79881de4fb01?requesterId=11111111-1111-1111-1111-111111111111` |
