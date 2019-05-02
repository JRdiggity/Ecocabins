$(document).ready(function () {
    // Capture AJAX start/end
    $body = $("body");

    $(document).on({
        ajaxStart: function () { $body.addClass("loading"); },
        ajaxStop: function () { $body.removeClass("loading"); }
    });

    // Handle contact form submission
    $("#frmContactForm").submit(function (e) {
        // Cache this
        const $this = $(this);

        // Prevent form submission
        e.preventDefault();

        // Check validity of form
        const isFormValid = $this.valid();

        if (isFormValid) {
            // Submit contact form via AJAX to server
            $.ajax({
                url: $this.attr('action'),
                type: $this.attr('method'),
                dataType: 'json',
                data: $this.serialize(),
                success: function (data) {
                    if (data === true) {
                        $("#contactDiv").addClass("d-none");
                        $("#contactResult").html("Thank you for your submission. Someone will contact you shortly.").removeClass("d-none someredclass");
                    } else {
                        showContactError();
                    }
                },
                error: function (xhr, err) {
                    showContactError();
                }
            });
        }

        return false;
    });
});

function showContactError() {
    $("#contactResult").html("Sorry, an error has occurred. Please try again later.").addClass("someredclass").removeClass("d-none");
}