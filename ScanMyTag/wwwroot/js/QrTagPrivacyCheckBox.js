function changePrivacy(tagId) {
    var qrTagId = tagId;
    //model.testId = tagId; //get the id of the checkbox which is the item id
    //var id = ('#privacyCheckBox' + tagId).toString();
    //if ($(id).prop('checked') == true) {  // set the value of the checked by id
    //    model.Enabled = true;
    //}
    //else {
    //    model.Enabled = false;
    //}
    $.ajax({
        type: "post",
        url: "/QRCode/ChangeQrPrivacy",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(qrTagId)

    });
}