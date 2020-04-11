$("#btn_Save").click(function (e) {
    e.preventDefault();

    var products=[]
    $.each($("input[type=checkbox]:checked"), function (index, element) {
        var data = {
            ProductId: $(element).next().val(),
            Quantity: $(element).parents("tr").find("input.Quantity").val(),
            Dosage: $(element).parents("tr").find("textarea").val()
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