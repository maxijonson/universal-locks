# Universal Locks

A Rust game plugin that allows you to lock anything with a container, even containers that don't exist yet!

Works with Oxide and Carbon.

## Features

- **Container Locking**: Anything that has a container becomes lockable.
- **Future-Proof**: Works with any container-based entity, not a fixed set of entities. Containers that don't exist yet will be lockable without needing an update.
- **Configurable Lock Position**: Vanilla lockable entities keep their lock position by default, but non-lockable entities can have their lock position configured instead of the default (0, 0, 0).

## Future Proof

Instead of having a hardcoded list of lockable entities, Universal Locks dynamically determines if an entity can be locked based on whether it has a container. This makes it future-proof and ensures that new container-based entities will automatically be supported without requiring plugin updates. Before Universal Locks was released publicly, it was used on my server for 2 years without ever needing to be changed.

## What Universal Locks Supports and Doesn't Support

Currently, Universal Locks' support is pretty straightforward: if it has a container (not attached to a vehicle), it can be locked. Anything else is not supported, but this may change in future updates (e.g: vehicle doors, vehicle containers are not supported yet).

## Configuration

The config file is `oxide/config/UniversalLocks.json` on Oxide and `carbon/configs/UniversalLocks.json` on Carbon.

```json
{
  "Lock all storage containers": true,
  "Exclude short names (When Lock all storage containers is enabled)": [
    "autoturret_deployed",
    "guntrap.deployed"
  ],
  "Include short names (When Lock all storage containers is disabled)": [],
  "Lock configuration by short name": {
    "composter": {
      "Position": {
        "x": 0.0,
        "y": 1.3,
        "z": 0.6
      },
      "Rotation": {
        "x": 0.0,
        "y": 90.0,
        "z": 0.0
      }
    },
    "dropbox.deployed": {
      "Position": {
        "x": 0.0,
        "y": 0.0,
        "z": 0.0
      },
      "Rotation": {
        "x": 0.0,
        "y": 0.0,
        "z": 0.0
      }
    },
    "vendingmachine.deployed": {
      "Position": {
        "x": 0.0,
        "y": 0.0,
        "z": 0.0
      },
      "Rotation": {
        "x": 0.0,
        "y": 0.0,
        "z": 0.0
      }
    },
    "furnace": {
      "Position": {
        "x": -0.02,
        "y": 0.3,
        "z": 0.5
      },
      "Rotation": {
        "x": 0.0,
        "y": 90.0,
        "z": 0.0
      }
    },
    "furnace.large": {
      "Position": {
        "x": 0.65,
        "y": 1.25,
        "z": -0.65
      },
      "Rotation": {
        "x": 0.0,
        "y": 45.0,
        "z": 0.0
      }
    },
    "refinery_small_deployed": {
      "Position": {
        "x": -0.01,
        "y": 1.25,
        "z": -0.6
      },
      "Rotation": {
        "x": 0.0,
        "y": 90.0,
        "z": 0.0
      }
    },
    "bbq.deployed": {
      "Position": {
        "x": 0.3,
        "y": 0.75,
        "z": 0.0
      },
      "Rotation": {
        "x": 0.0,
        "y": 0.0,
        "z": 0.0
      }
    },
    "planter.small.deployed": {
      "Position": {
        "x": 0.0,
        "y": 0.45,
        "z": 0.0
      },
      "Rotation": {
        "x": 0.0,
        "y": 0.0,
        "z": 90.0
      }
    },
    "planter.large.deployed": {
      "Position": {
        "x": 0.0,
        "y": 0.45,
        "z": 0.0
      },
      "Rotation": {
        "x": 0.0,
        "y": 0.0,
        "z": 90.0
      }
    },
    "hitchtrough.deployed": {
      "Position": {
        "x": 0.0,
        "y": 0.4,
        "z": 0.3
      },
      "Rotation": {
        "x": 0.0,
        "y": 90.0,
        "z": 0.0
      }
    },
    "mixingtable.deployed": {
      "Position": {
        "x": 0.0,
        "y": 0.8,
        "z": 0.38
      },
      "Rotation": {
        "x": 0.0,
        "y": 90.0,
        "z": 0.0
      }
    },
    "researchtable_deployed": {
      "Position": {
        "x": 0.0,
        "y": 0.8,
        "z": 0.38
      },
      "Rotation": {
        "x": 0.0,
        "y": 90.0,
        "z": 0.0
      }
    }
  },
  "Version": {
    "Major": 1,
    "Minor": 0,
    "Patch": 0
  }
}
```

## A Lock Master Rewrite

Credits to [Lock Master by FastBurst](https://umod.org/plugins/lock-master) for the original concept. 

I originally created Universal Locks in December 2024 as a private rewrite of Lock Master v1.1.8 (October 2021), with the sole purpose of rewriting it from Lock Master's hardcoded entity-based system to a generic container-based system. The result of the rewrite was a 5x smaller plugin while being future proof to any container-based entity that may exist in the game. Lock Master later got its own generic system in v1.1.13 (July 2026). 

As of Lock Master 1.1.13, Universal Locks still has other advantages over Lock Master:
  - **Owner checks**: only player-owned containers can be locked. Lock Master has no restriction, allowing anyone with building privileges to lock any world-owned containers, such as air drops and road barrels.
  - **Exclusions**: Universal Locks allows you to exclude specific short names from being locked when the "Lock all storage containers" option is enabled. Lock Master only allows you to specify pre-defined groups of items to make lockable.
  - **Lock Position Configuration**: Universal Locks allows you to configure the exact position and rotation of locks on each container type. Lock Master hardcodes theirs for specific entities.
