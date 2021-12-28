$(document).ready(function() {
  $('.sideMenuToggler').on('click', function() {
    $('.wrapper').toggleClass('active');
  });

  var adjustSidebar = function() {
    $('.sidebar').slimScroll({
      height: document.documentElement.clientHeight - $('.navbar').outerHeight()
    });
  };

  adjustSidebar();
  $(window).resize(function() {
    adjustSidebar();
  });
});

function ImagesFileAsURL() {
  var fileSelected = document.getElementById('image').files;
  if(fileSelected.length > 0) {
    var fileToLoad = fileSelected[0];
    var fileReader = new FileReader();
    fileReader.onload = function(fileLoaderEvent) {
      var srcData = fileLoaderEvent.target.result;
      var newImage = document.getElementById('display-image');
      newImage.src = srcData;
    }
    fileReader.readAsDataURL(fileToLoad);
  }
}
