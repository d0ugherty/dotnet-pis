var map;
var trainLayer, stationLayer, trolleyLayer, trolleyStopLayer;

document.addEventListener('DOMContentLoaded', function() {
    initializeMap(39.9628399, -75.148437);
    displayMapShapes("Rail", "SEPTA");
});

function initializeMap(lat, lon) {
    map = L.map('map').setView([lat, lon], 13);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    trainLayer = L.layerGroup();
    stationLayer = L.layerGroup().addTo(map);
    trolleyLayer = L.layerGroup().addTo(map);
    trolleyStopLayer = L.layerGroup().addTo(map);
}

function displayMapShapes(routeType, agencyName) {
    var points = [];
    
    $.ajax({
        url: `Map/GetShapeData?routeType=${routeType}&agencyName=${agencyName}`,
        method: 'GET',
        success: function(shapeData) {
            
            for(const point of shapeData){
                points.push([point["shapePtLat"], point["shapePtLon"]]);
            }
            
            L.polyline(points, { color: '#43647c'}).addTo(map);
        }
    });
}