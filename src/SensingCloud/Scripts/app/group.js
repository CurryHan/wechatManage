function fillParentGroupDDL(targetGroupType,ddlID) {
        var options = {
            url: "/group/GetParentGroupByType",
            method: "get",
            data: { targetGroupType: targetGroupType }
        }
        $.ajax(options).success(function (response) {
            if (response.data) {
                $("#"+ddlID).empty();
                for (var i = 0; i < response.data.length; i++) {
                    var group = response.data[i];
                    $("<option value='" + group.value + "'>" + group.name + "</option>").appendTo($("#" + ddlID));
                }
            }
        });
}

function fillAcivityDDL(groupID, ddlID) {
    var options = {
        url: "/Activity/GetActivityByGroup",
        method: "get",
        data: { groupID: groupID }
    }
    $.ajax(options).success(function (response) {
        if (response.data) {
            $("#" + ddlID).empty();
            for (var i = 0; i < response.data.length; i++) {
                var group = response.data[i];
                $("<option value='" + group.value + "'>" + group.name + "</option>").appendTo($("#" + ddlID));
            }
        }
    });
}