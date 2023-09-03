# Configure the Azure provider
terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.0.2"
    }
  }

  required_version = ">= 1.1.0"
}

provider "azurerm" {
  features {}

  subscription_id = "cd4194f2-872c-4eff-ad97-f9ac80cf95ef"
}

resource "azurerm_resource_group" "rg" {
  name     = "azureresume"
  location = "westus"
}

