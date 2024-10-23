function selectVehicle(vehicleType, elementId) {
    // guarda la selección en el campo oculto
    document.getElementById('<%= hfTipoVehiculo.ClientID %>').value = vehicleType;

    // remover la clase seleccionada de todos los iconos
    var vehicleIcons = document.getElementsByClassName("vehicle-option");
    for (var i = 0; i < vehicleIcons.length; i++) {
        vehicleIcons[i].classList.remove("selected");
    }

    // añade la clase seleccionada al icono elegido
    document.getElementById(elementId).parentElement.classList.add("selected");
}
