
$("body").on("click", "#btnSaveOrder", function (e) {
    e.preventDefault();
    var array = [];
    $.each($("tbody tr"), function (indexInArray, ele) {
        array.push({
            ProductId: $(ele).find("input[type='hidden']").val(),
            Quantity: $(ele).find("td:eq(1)").text(),
            UnitCost: $(ele).find("td:eq(2)").text()
        });
    });
    debugger
    var data = {
        DocId: $("#DocId").val(),
        ListProducts: array,
        FullName: $("#FullName").val(),
        Email: $("#Email").val(),
        Mobile: $("#Mobile").val(),
        Address: $("#Address").val()
    }
    $.ajax({
        type: "POST",
        url: "/Home/SaveOrderWithDoctor",
        data: data,
        dataType: "json",
        success: function (response) {
            if (response.IsSuccess) {
                alert("Thành Công");
                window.location.href = "/Home/index";

            } else {
                alert("Có Lỗi");
            }
        }
    });

});