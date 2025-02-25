$(document).on('click', '.btnCart', function () {
    debugger
    var obj = {
        Id: $(this).data('key'),
        ProductName: $(this).data('name'),
        UnitPrice: $(this).data('price'),
        Quantity: 1,
        Total: $(this).data('price')
    }
    var oldItems = JSON.parse(localStorage.getItem("Products") || '[]')
    var filterItems = oldItems.filter(x => x.Id == obj.Id)
    if (filterItems && filterItems.length > 0) {
        toastr.error("Item Already Added In the Cart");
        return;
    }
    oldItems.push(obj);
    localStorage.setItem("Products", JSON.stringify(oldItems));
    toastr.success("Item Added In The Cart");
})
function loadItems() {
    var html = ''
    var products = localStorage.getItem("Products");
    var products = JSON.parse(products);
    $.each(products, function (key, item) {
        $("#listDatas").find("tr").remove();
        html += "<tr>" +
            "<td>" + item.Id + "</td>" +
            "<td>" + item.ProductName + "</td>" +
            "<td>" + item.UnitPrice + "</td>" +
            "<td>" + "<input type='number' class='form-control txtQuantity' data-key='" + item.Id + "' value='" + item.Quantity + "'/>" + "</td>" +
            "<td>" + item.Total + "</td>" +
            "<td>" +
            "<button type='button' data-key='" + item.Id + "' class='btn btn-danger btn-sm btnDelete'>" +
            "<i class='fa fa-trash'></i> Delete" +
            "</button>" +
            "</td>" +
            "</tr>";
    });
    $("#listDatas").append(html);
}
$(document).on('click', '.btnRemoveProducts', function () {
    Swal.fire({
        title: "Are you sure want to reset cart?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, Reset it!"
    }).then((result) => {
        if (result.isConfirmed) {
            localStorage.removeItem("Products");
            loadItems();
            toastr.success("Products Removed Successfully");
        }
    });
})
$(document).on('click', '.btnDelete', function () {
    Swal.fire({
        title: "Are you sure want to remove this item?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, Reset it!"
    }).then((result) => {
        if (result.isConfirmed) {
            var id = $(this).data('key');
            var oldProducts = JSON.parse(localStorage.getItem("Products") || '[]');
            oldProducts = oldProducts.filter(x => x.Id != id)
            localStorage.setItem("Products", JSON.stringify(oldProducts));
            loadItems();
        }
    });
})
$(document).on('change', '.txtQuantity', function () {
    var id = $(this).data('key');
    var newQuantity = $(this).val();
    var oldProducts = JSON.parse(localStorage.getItem("Products") || '[]');
    var s = oldProducts.filter(x => x.Id == id);
    if (s.length <= 0) {
        toastr.error("Item Not Found");
        return;
    }
    var selectedItem = s[0];
    selectedItem.Quantity = newQuantity;
    selectedItem.Total = +newQuantity * +selectedItem.UnitPrice;
    localStorage.setItem("Products", JSON.stringify(oldProducts));
    loadItems();
})

$(document).on('click', '.btnCheckout', function () {
    $(".modal").modal("show");
})

$(document).on('click', '.modalSave', function () {
    debugger
    var masterValue = {
        FullName: $(".txtFullName").val() || '',
        MobileNo: $(".txtMobileNo").val() || '',
        Address: $(".txtAddress").val() || '',
        Email: $(".txtEmail").val() || '',
    }
    if (masterValue.FullName == '') {
        toastr.error("Enter FullName")
    }
    else if (masterValue.Email == '') {
        toastr.error("Enter Email")
    }
    else if (masterValue.MobileNo == '') {
        toastr.error("Enter MobileNo")
    }
    else if (masterValue.Address == '') {
        toastr.error("Enter Address")
    }
    else {
        var detailValue = localStorage.getItem("Products") || '[]';
        var detailValue = JSON.parse(detailValue);
        var payload = {
            master : masterValue,
            detail : detailValue
        }
        $.ajax({
            method: 'post',
            url: "/Order/Save",
            data: JSON.stringify(payload),
            contentType: "application/json;charset=utf-8",
            success: function (res) {
                debugger;
            }
        })
    }
})