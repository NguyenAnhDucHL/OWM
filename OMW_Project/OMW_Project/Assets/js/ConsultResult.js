$("#btn_Save").click(function (e) {
    e.preventDefault();

    var products = []
    $.each($("#mytbody tr"), function (index, element) {
        var data = {
            ProductId: $(element).find("input[type='hidden']").val(),
            Quantity: $(element).find("input.Quantity").val(),
            Dosage: $(element).find("textarea").val()
        }
        products.push(data)
    });
    var consultResult = {
        ConsultingId: $("#ConsultingId").val(),
        ContactMedia: $("#ContactMedia").val(),
        ContactResult: $("#ContactResult").val(),
        Suggestion: $("#Suggestion").val(),
        TreamentDuration: $("#TreamentDuration").val(),
        ContactTime: $("#ContactTime").val(),
        NextContactTime: $("#NextContactTime").val(),
        ProductSuggests: products
    }
    $.ajax({
        type: "POST",
        url: "/ConsultResult/Create",
        data: {
            consultResult: consultResult
        },
        dataType: "json",
        success: function (response) {
            if (response.IsSuccess) {
                alert("Thành công");
                window.location.replace("/Identity/ConsultResult");
            }
        }
    });
});
$(".delete_Item").click(function (e) {
    e.preventDefault();
    
});
$("body").on("click", ".delete_Item", function (e) {
    e.preventDefault();
    $(this).parents("tr").remove()
    calculatePrice();
});
$("body").on("change", "#mytbody input[type='number']", function (e) {
    e.preventDefault();
    calculatePrice();
});
$(".add_product").click(function (e) {
    e.preventDefault();
    var obj = {
        ProductName: $(this).parents("tr").find("td:eq(1)").text(),
        ProductId: $(this).next().val(),
        Image: $(this).parents("tr").find("img").attr("src"),
        Price: $(this).parents("tr").find("td:eq(3)").text()
    }

    var elements = $("#mytbody").find("input[value='" + obj.ProductId + "']");
    if (elements.length > 0) {
        var current = elements.next().val() * 1;
        elements.next().val(current+1)
        debugger
    } else {
        var html = "<tr>"
            + "<td><img src='" + obj.Image + "' height='100' /></td>"
            + "<td style='position:relative'>"
            + "<p style='font-size:22px;font-weight:bold;margin-bottom:35px'>" + obj.ProductName + "</p>"
            + "<div style=;position:absolute;bottom:10px;'>"
            + "<span>Số lượng :</span>"
            + "<input type='hidden' value='" + obj.ProductId + "' />"
            + "<input style='width:auto;display:inline' type='number' value='1' min='1' class='form-control Quantity' />"
            + "</div>"
            + "</td>"
            + "<td>" + obj.Price + "</td>"
            + "<td>"
            + "<textarea class='form-control Dosage' style='min-height:100px'></textarea>"
            + "</td>"
            + "<td>"
            + "<button class='btn btn-danger delete_Item'>Xóa</button>"
            + "</td>"
            + "</tr >";
        $("#mytbody").append(html)
    }
    calculatePrice();
});
calculatePrice = () => {
    var total = 0;
    $.each($("#mytbody input[type='number']"), function (index, ele) {
        var price = $(ele).parents("td").next().text() * 1;
        var quality = $(ele).val() * 1;
        total += (price * quality)
        debugger
    });
    $("#totalMoney").val(total)
}
