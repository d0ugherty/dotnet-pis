var map;
var trainLayer, stationLayer, trolleyLayer, trolleyStopLayer;

const RouteColors = Object.freeze({
    SEPTA_RR: Symbol('#43647c')
});

document.addEventListener('DOMContentLoaded', function() {
    
    initializeMap(39.9628399, -75.148437);
    
    displayMapShapes("Rail", "SEPTA");
    
    displayStopMarkers("Rail", "SEPTA");
    
    displayTrainMarkers(trainLayer);
    
    setInterval(displayTrainMarkers, 5000, trainLayer);
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
    
    $.ajax({
        url: `Map/GetShapeData?routeType=${routeType}&agencyName=${agencyName}`,
        method: 'GET',
        success: function(shapeData) {
            
            Object.keys(shapeData).forEach(key => {
                const points = [];
                
                let shape = shapeData[key]; 
                
                for(const point of shape){
                    points.push([point["shapePtLat"], point["shapePtLon"]]);
                }

                L.polyline(points, { color: '#43647c'}).addTo(map);
                
            });
        }
    });
}

function displayTrainMarkers(trainLayer){
    trainLayer.clearLayers();
    
    trainLayer.addTo(map);
    
    $.ajax({
        url: `Map/GetTrainData/`,
        method: 'GET',
        success: function(trainData) {
            
            for(const train of trainData){

                let trainMarker = L.marker([train["latitude"], train["longitude"]]).addTo(trainLayer);
                
                let popup = L.popup({ "autoClose" : false, "closeOnClock" : null});
                
                let content = `<b>Train No. </b> ${train["trainNumber"]}<br>
                                <b>Next Stop: </b> ${train["nextStop"]} <br>
                                <b>Line: </b> ${train["line"]}<br>
                                <b> Destination: </b> ${train["destination"]}`;
                
                popup.setContent(content);
                
                trainMarker.bindPopup(popup);
            }
        }
    });
        
}

function displayStopMarkers(routeType, agencyName){
    
    $.ajax({
        url:`Map/GetStops?routeType=${routeType}&agencyName=${agencyName}`,
        method: 'GET',
        success: function(stops) {
            for(const stop in stops){
                console.log(stop);
            }
        }
    })
}