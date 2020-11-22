$("[data-post]").on("click", function () {

    var data = {
        html: "<h1>Javascripts banner.</h1>",
    }

    $.ajax({
        type: "POST",
        url: "/banner/create",
        data: data,
        success: function (resp) {
            alert(resp);
        },
        error: function (resp) {
            alert(resp);
        }
    });

});