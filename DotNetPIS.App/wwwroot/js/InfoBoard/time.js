document.addEventListener('DOMContentLoaded', function() {
    
    getTime();
    setInterval(getTime, 1000);

});

function getTime(){
    const now = new Date();
    const timeString = now.toLocaleTimeString();
    
    document.getElementById('current-date').innerHTML = timeString;
}

