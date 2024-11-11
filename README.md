# 3D Product Shelf - Unity WebGL Demo

This project is a demo of a 3D Product Shelf developed using Unity. It displays product information dynamically fetched from a server, with interactive features for editing product details. This project was completed as part of an assignment for a Unity Developer position.

## Project Overview

The **3D Product Shelf** showcases between 1 to 3 products in a rotating display. Products are fetched dynamically from a server using an API. Users can interact with each product by modifying the name, price and description, with changes immediately reflected on the shelf.

### Key Features

1. **Dynamic Product Display**: Products are fetched from a server, which returns between 1 and 3 randomly selected products from a predefined list of 10.
2. **User Interaction**:
   - Users can edit the name, price, and description of each product.
   - Upon submitting changes, the updated information is shown in real-time on the product display.
3. **Interactive Shelf**: The shelf is implemented as a 3D rotating platform, allowing users to navigate through the displayed products.
4. **WebGL Compatibility**: Designed to be embedded in websites, ensuring compatibility with both desktop and mobile browsers.

---

---

## Running the Project Locally

To test the project locally, follow these steps:

1. **Clone the Repository**:
   ```
   git clone <repository-url>
   ```
2. **Open WebGL Folder**:
   - Open the `WebGL` folder in the repository.

3. **Run the Play.bat File**:
   - Double-click the provided `Play.bat` file to run a local server and open the project in your default browser.
   - This script uses a simple Python server to serve the built WebGL files, allowing access from your local network.
   - A command prompt window will open, showing the IP address and port for connecting from another device if needed.

---

## Code Structure and Design Decisions

### Major Scripts Overview

1. **ProductManager.cs**
   - **Purpose**: Handles fetching product data from the server and managing the product queue.
   - **Functionality**:
     - Uses `UnityWebRequest` to communicate with the server API and parse the JSON response.
     - Stores and manages products using `SerializableDictionary<string, Product>` and queues products for display.
     - Emits an `OnFinishedProcessing` event after processing all product data, indicating that products are ready for display.

2. **PlatformManager.cs**
   - **Purpose**: Manages the interaction between the product manager and the platform.
   - **Functionality**:
     - Controls the initialization and rotation of products displayed on the platform.
     - Subscribes to product processing events from `ProductManager` and handles the transition between different products.
      
3. **ProductEditor.cs**
   - **Purpose**: Provides a UI for editing product details.
   - **Functionality**:
     - Users can toggle between viewing and editing modes for a selected product.
     - Displays input fields for editing product name, price, and description, and saves changes.
     - Updates the `ShowcaseSpot` to reflect the new product information.
       
4. **Platform.cs**
   - **Purpose**: Represents the 3D rotating platform that holds the product showcase spots.
   - **Functionality**:
     - Provides methods to rotate the platform to the next or previous product.
     - Keeps track of the current spot and handles the rotation logic to switch between products smoothly.

5. **ShowcaseSpot.cs**
   - **Purpose**: Represents a spot on the platform where a product is displayed.
   - **Functionality**:
     - Initializes the product model and updates product details such as name, price, and description.
     - Handles enabling and disabling the product visualization during interactions.
     - Supports edit mode to move the product for better user interaction when editing details.

6. **Button3D.cs**
   - **Purpose**: Represents interactive buttons that can be placed in the 3D scene.
   - **Functionality**:
     - Implements the `IPointerHandler` interface to handle click events.
     - Uses Unity's `UnityEvent` to add custom actions when the button is clicked.

7. **ClickHandler.cs**
   - **Purpose**: Handles user clicks in the 3D environment using Unity's new Input System.
   - **Functionality**:
     - Uses raycasting to determine which object is being clicked.
     - Calls `OnPointerClick()` on objects implementing the `IPointerHandler` interface (e.g., `Button3D`).

---

### External Libraries/Assets

- **Python Standard Library**: Used to run the local HTTP server via the `Play.bat` file.
- **TextMeshPro (TMP)**: Provides enhanced text rendering for displaying product details.
- **Glasses Models**: Used as an example for the product displayed glasses models where downloaded from TurboSquid & Free3D.
- **HDRI**: Used for the skybox and glasses reflections downloaded from PolyHaven.

### Design Decisions

1. **Singleton Pattern**: The `MonoSingleton<T>` pattern was used for classes like `ProductManager`, `PlatformManager`, and `ProductEditor`. This ensures only one instance of each exists, providing a global point of access, making it easier to manage and access shared data.
2. **Event-Driven Communication**: To improve the separation of concerns, events are used to communicate between scripts. For instance, `OnFinishedProcessing` in `ProductManager` allows other scripts to react when products are ready without tightly coupling them.
3. **SOLID Principles**: The project follows SOLID principles to ensure maintainability and scalability. Each class has a single responsibility, open for extension, and designed to be modular and loosely coupled for easy modification and extension.
4. **Edit Mode**: The `ProductEditor` provides a user-friendly interface to update product details. The `ToggleEditMode()` functionality gives users a simple way to switch between browsing and editing.
5. **Platform and Spot System**: The rotating platform with showcase spots was chosen to make the shelf visually appealing and intuitive for users to interact with multiple products.

