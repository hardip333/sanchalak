app.directive("instanceparttermSelect", function () {
    return {
        templateUrl: "UI/components/instanceparttermSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.IncProgrammePartTermGetActiveList = [];
            $scope.defaultInstancePartTermList = [];
            var termObj = {
                Id : 0,
                InstancePartTermName: "Select Instance PartTerm"
            }
            $scope.defaultInstancePartTermList.push(termObj);

            $scope.IncProgrammePartTermGetActive = function (data) {

                $http({
                    method: 'POST',
                    url: 'api/IncProgrammePartTerm/IncProgrammePartTermGetActive',
                    data: { ProgrammeInstanceId: data},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                        }
                        else {

                            $scope.tempList = response.obj;
                            $scope.IncProgrammePartTermGetActiveList = $scope.defaultInstancePartTermList.concat($scope.tempList);
                       
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
       /*     $scope.IncProgrammePartTermGetActive();*/


        }],
        link: function (e, t, a) {
        }
    }
})