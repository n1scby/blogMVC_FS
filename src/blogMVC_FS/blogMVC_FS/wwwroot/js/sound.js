
var barkButton = document.getElementById("bark-bark");

barkButton.addEventListener("click", function(){
    var barkSound = new Audio("/sounds/dogbark.wav");
    barkSound.play();

})

