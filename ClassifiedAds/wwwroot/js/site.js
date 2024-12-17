$(document).on('change', 'input[type=file]', (function (event) {
    input = $(this);
    var file = event.target.files[0]; // Get the selected file
    if (file) {
        var reader = new FileReader();

        reader.onload = function (e) {
            // Create an image element and set its source to the file's URL
            var img = $("<img>").attr("src", e.target.result).css("max-width", "300px");

            //$("#imageContainer").html(img); // Append the image to the container
            $(input).closest('div').find('.img-preview').remove();
            $(input).closest('div').append('<div class="img-preview"></div>');
            $(input).closest('div').find('.img-preview').html(img);
        };
        // Read the image file as a data URL
        reader.readAsDataURL(file);
    }
}));