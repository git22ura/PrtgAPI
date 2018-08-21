﻿. $PSScriptRoot\..\..\Support\PowerShell\UnitTest.ps1

Describe "Get-NotificationAction" -Tag @("PowerShell", "UnitTest") {

    It "can deserialize" {
        $actions = Get-NotificationAction
        $actions.Count | Should Be 1
    }

    It "can filter by Id" {

        $expected = @(
            "api/table.xml?content=notifications&columns=objid,name,baselink,tags,type,active,basetype&count=*&filter_objid=301&",
            "controls/objectdata.htm?id=300&objecttype=notification&"
        )

        SetAddressValidatorResponse $expected

        Get-NotificationAction -Id 301
    }

    It "can filter by name" {
        $expected = @(
            "api/table.xml?content=notifications&columns=objid,name,baselink,tags,type,active,basetype&count=*&filter_name=@sub(admin)&",
            "controls/objectdata.htm?id=300&objecttype=notification&"
        )

        $response = SetAddressValidatorResponse $expected
        $dictionary = GetCustomCountDictionary @{
            Notifications = 1
        }
        $response.CountOverride = $dictionary

        Get-NotificationAction *admin*
    }

    It "can filter by tags" {
        $obj1 = GetItem
        $obj2 = GetItem

        $obj1.ObjId = 300
        $obj1.Tags = "testappletest","peaches"
        $obj2.ObjId = 301
        $obj2.Tags = "testappletest"

        WithItems ($obj1, $obj2) {
            $actions = Get-NotificationAction -Tags *apple*,"peaches"
            $actions.Count | Should Be 1
        }
    }

    It "can filter by tag" {
        $obj1 = GetItem
        $obj2 = GetItem
        $obj3 = GetItem

        $obj1.ObjId = 300
        $obj1.Tags = "testappletest","peaches"
        $obj2.ObjId = 301
        $obj2.Tags = "testbananatest"
        $obj3.ObjId = 302
        $obj3.Tags = "testappletest"

        WithItems ($obj1, $obj2, $obj3) {
            $actions = Get-NotificationAction -Tag *apple*
            $actions.Count | Should Be 2
        }
    }
}