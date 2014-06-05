#EnergyTrading-MDM

Implementation of the EnergyTrading Master Data Management Service layer.

The service layer is currently restricted to using EntityFramework for persistence, which results in a need to define some concrete objects where we would like to have interface definitions and allow people to inject their own implementations.
