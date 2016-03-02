(function () {
    $("#filterToggle").click(function () {
        $("#filterForm").toggle("fast", function () {
            $("#filterToggle").text($("#filterForm").is(":visible") ? "hide filter" : "show filter");
        });        
    });
})();