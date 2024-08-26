function updateApprovalStatus(insuredPolicyId) {
    var approvalStatus = $('#approvalStatus').val();
    $.ajax({
        type: 'POST',
        url: '@Url.Action("UpdateApprovalStatus", "PolicyRequest")',
        data: { insuredPolicyId: insuredPolicyId, approvalStatus: approvalStatus },
        success: function (data) {
            alert('Approval status updated successfully.');
            $('#reviewModal').modal('hide');
            location.reload();
        },
        error: function (xhr, status, error) {
            alert('Failed to update approval status.');
        }
    });
}