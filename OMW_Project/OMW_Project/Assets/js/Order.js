

$("#btnSaveOrder").click(function (e) {
    e.preventDefault();
    var carts = JSON.parse(sessionStorage.getItem('shoppingCart'));
    debugger
    var array = [];
    $.each(carts, function (indexInArray, valueOfElement) {
        array.push({
            ProductId: valueOfElement.Id,
            Quantity: valueOfElement.count,
            UnitCost: valueOfElement.price
        });
    });
    var data = {
        ListProducts: array,
        FullName: $("#FullName").val(),
        Email: $("#Email").val(),
        Mobile: $("#Mobile").val(),
        Address: $("#Address").val()
    }
    $.ajax({
        type: "POST",
        url: "/Home/SaveOrder",
        data: data,
        dataType: "json",
        success: function (response) {
            if (response.IsSuccess) {
                alert("Thành Công");
                shoppingCart.clearCart();
                window.location.href = "/Home/index";

            } else {
                alert("Có Lỗi");
            }
        }
    });
    debugger
});