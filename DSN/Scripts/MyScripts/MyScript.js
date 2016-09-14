$(".nav li").on("click", function () {
    //$(".nav").find(".active").removeClass("active");
    $(this).addClass("active");
});
//$(".btn-success").on("click", function () {
//    $.ajax(
//    {
//        url: "@Url.Action(Home,Approve)"
//    });
//    $(this).parent().parent().children(".status").text("Approved");
//    $(this).replaceWith("None");
//});